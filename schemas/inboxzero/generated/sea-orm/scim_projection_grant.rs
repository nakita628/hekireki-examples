use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "scimProjectionGrant")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    #[sea_orm(column_name = "connectionId")]
    pub connection_id: String,
    #[sea_orm(column_name = "provisioningDomainId")]
    pub provisioning_domain_id: String,
    #[sea_orm(column_name = "scimUserId")]
    pub scim_user_id: String,
    #[sea_orm(column_name = "userId")]
    pub user_id: String,
    #[sea_orm(column_name = "sourceKind")]
    pub source_kind: String,
    #[sea_orm(column_name = "sourceId")]
    pub source_id: String,
    #[sea_orm(column_name = "sourceValue")]
    pub source_value: Option<String>,
    pub role: String,
    #[sea_orm(unique)]
    #[sea_orm(column_name = "grantKey")]
    pub grant_key: String,
    #[sea_orm(column_name = "createdAt")]
    pub created_at: DateTime,
    #[sea_orm(column_name = "updatedAt")]
    pub updated_at: DateTime,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::scim_user::Entity",
        from = "Column::ScimUserId",
        to = "super::scim_user::Column::Id",
        on_delete = "Cascade"
    )]
    ScimUser,
    #[sea_orm(
        belongs_to = "super::user::Entity",
        from = "Column::UserId",
        to = "super::user::Column::Id",
        on_delete = "Cascade"
    )]
    User,
}

impl Related<super::scim_user::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::ScimUser.def()
    }
}

impl Related<super::user::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::User.def()
    }
}

impl ActiveModelBehavior for ActiveModel {}
