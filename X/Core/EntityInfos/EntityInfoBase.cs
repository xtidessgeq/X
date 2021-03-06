﻿using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;

using X.Data;
using X.Entity;
using X.Entity.Database;
using X.Extensions;
using X.Json;
using X.Reflection;

namespace X.Core.EntityInfos
{
    /// <summary>
    /// 实体信息基类
    /// </summary>
    public abstract class EntityInfoBase : EntityBase<Guid>, IEntityInfo
    {
        /// <summary>
        /// 获取或设置 实体名称
        /// </summary>
        [Required, DisplayName("实体名称")]
        public string Name { get; set; }

        /// <summary>
        /// 获取或设置 实体类型名称
        /// </summary>
        [Required, DisplayName("实体类型名称")]
        public string TypeName { get; set; }

        /// <summary>
        /// 获取或设置 是否启用数据审计
        /// </summary>
        [DisplayName("是否数据审计")]
        public bool AuditEnabled { get; set; } = true;

        /// <summary>
        /// 获取或设置 实体属性信息JSON字符串
        /// </summary>
        [Required, DisplayName("实体属性信息Json字符串")]
        public string PropertyJson { get; set; }

        /// <summary>
        /// 获取 实体属性信息
        /// </summary>
        [NotMapped]
        public EntityProperty[] Properties
        {
            get
            {
                if (string.IsNullOrEmpty(PropertyJson) || !PropertyJson.StartsWith("["))
                {
                    return new EntityProperty[0];
                }
                return PropertyJson.FromJsonString<EntityProperty[]>();
            }
        }

        /// <summary>
        /// 从实体类型初始化实体信息
        /// </summary>
        /// <param name="entityType"></param>
        public virtual void FromType(Type entityType)
        {
            Check.NotNull(entityType, nameof(entityType));

            TypeName = entityType.GetFullNameWithModule();
            Name = entityType.GetDescription();
            AuditEnabled = true;

            PropertyInfo[] propertyInfos = entityType.GetProperties(BindingFlags.Instance | BindingFlags.Public);
            string[] exceptNames = { "DeletedTime" };
            PropertyJson = propertyInfos.Where(m => !exceptNames.Contains(m.Name)
                    && (m.GetMethod != null && !m.GetMethod.IsVirtual || m.SetMethod != null && !m.SetMethod.IsVirtual))
                .Select(property =>
                {
                    EntityProperty ep = new EntityProperty()
                    {
                        Name = property.Name,
                        Display = property.GetDescription(),
                        TypeName = property.PropertyType.FullName
                    };
                    //枚举类型，获取枚举项作为取值范围
                    if (property.PropertyType.IsEnum)
                    {
                        ep.TypeName = typeof(int).FullName;
                        Type enumType = property.PropertyType;
                        Array values = enumType.GetEnumValues();
                        int[] intValues = values.Cast<int>().ToArray();
                        string[] names = values.Cast<Enum>().Select(m => m.ToDescription()).ToArray();
                        for (int i = 0; i < intValues.Length; i++)
                        {
                            string value = intValues[i].ToString();
                            ep.ValueRange.Add(new { id = value, text = names[i] });
                        }
                    }

                    if (property.HasAttribute<UserFlagAttribute>())
                    {
                        ep.IsUserFlag = true;
                    }

                    return ep;
                }).ToArray().ToJsonString();
        }
    }
}