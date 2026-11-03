use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "OrganisationGroupMember")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    #[sea_orm(column_name = "groupId")]
    pub group_id: String,
    #[sea_orm(column_name = "organisationMemberId")]
    pub organisation_member_id: String,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::organisation_group::Entity",
        from = "Column::GroupId",
        to = "super::organisation_group::Column::Id",
        on_delete = "Cascade"
    )]
    Group,
    #[sea_orm(
        belongs_to = "super::organisation_member::Entity",
        from = "Column::OrganisationMemberId",
        to = "super::organisation_member::Column::Id",
        on_delete = "Cascade"
    )]
    OrganisationMember,
}

impl Related<super::organisation_group::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Group.def()
    }
}

impl Related<super::organisation_member::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::OrganisationMember.def()
    }
}

impl ActiveModelBehavior for ActiveModel {}
