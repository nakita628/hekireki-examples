use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "scimUser")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    #[sea_orm(column_name = "connectionId")]
    pub connection_id: String,
    #[sea_orm(column_name = "provisioningDomainId")]
    pub provisioning_domain_id: String,
    #[sea_orm(column_name = "userId")]
    pub user_id: String,
    #[sea_orm(unique)]
    #[sea_orm(column_name = "connectionUserKey")]
    pub connection_user_key: String,
    #[sea_orm(column_name = "userName")]
    pub user_name: String,
    #[sea_orm(unique)]
    #[sea_orm(column_name = "userNameKey")]
    pub user_name_key: String,
    #[sea_orm(column_name = "primaryEmail")]
    pub primary_email: String,
    #[sea_orm(column_name = "workEmailValueIndex")]
    pub work_email_value_index: String,
    #[sea_orm(column_name = "emailValueIndex")]
    pub email_value_index: String,
    #[sea_orm(column_name = "displayName")]
    pub display_name: String,
    #[sea_orm(column_name = "formattedName")]
    pub formatted_name: String,
    #[sea_orm(column_name = "givenName")]
    pub given_name: Option<String>,
    #[sea_orm(column_name = "familyName")]
    pub family_name: Option<String>,
    #[sea_orm(column_name = "serializedEmails")]
    pub serialized_emails: String,
    #[sea_orm(column_name = "serializedAttributes")]
    pub serialized_attributes: Option<String>,
    #[sea_orm(column_name = "externalId")]
    pub external_id: Option<String>,
    #[sea_orm(unique)]
    #[sea_orm(column_name = "externalIdKey")]
    pub external_id_key: Option<String>,
    pub active: bool,
    #[sea_orm(unique)]
    #[sea_orm(column_name = "orderKey")]
    pub order_key: String,
    #[sea_orm(column_name = "createdAt")]
    pub created_at: DateTime,
    #[sea_orm(column_name = "updatedAt")]
    pub updated_at: DateTime,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::user::Entity",
        from = "Column::UserId",
        to = "super::user::Column::Id",
        on_delete = "Cascade"
    )]
    User,
    #[sea_orm(has_many = "super::scim_projection_grant::Entity")]
    ScimProjectionGrants,
    #[sea_orm(has_many = "super::scim_group_member::Entity")]
    ScimGroupMembers,
}

impl Related<super::user::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::User.def()
    }
}

impl Related<super::scim_projection_grant::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::ScimProjectionGrants.def()
    }
}

impl Related<super::scim_group_member::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::ScimGroupMembers.def()
    }
}

impl ActiveModelBehavior for ActiveModel {}
