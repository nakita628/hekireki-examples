use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "scimGroupMember")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    #[sea_orm(column_name = "connectionId")]
    pub connection_id: String,
    #[sea_orm(column_name = "groupId")]
    pub group_id: String,
    #[sea_orm(column_name = "scimUserId")]
    pub scim_user_id: String,
    #[sea_orm(unique)]
    #[sea_orm(column_name = "membershipKey")]
    pub membership_key: String,
    #[sea_orm(column_name = "createdAt")]
    pub created_at: DateTime,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::scim_group::Entity",
        from = "Column::GroupId",
        to = "super::scim_group::Column::Id",
        on_delete = "Cascade"
    )]
    ScimGroup,
    #[sea_orm(
        belongs_to = "super::scim_user::Entity",
        from = "Column::ScimUserId",
        to = "super::scim_user::Column::Id",
        on_delete = "Cascade"
    )]
    ScimUser,
}

impl Related<super::scim_group::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::ScimGroup.def()
    }
}

impl Related<super::scim_user::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::ScimUser.def()
    }
}

impl ActiveModelBehavior for ActiveModel {}
