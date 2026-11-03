use super::organisation_group_type::OrganisationGroupType;
use super::organisation_member_role::OrganisationMemberRole;
use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "OrganisationGroup")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    pub name: Option<String>,
    pub r#type: OrganisationGroupType,
    #[sea_orm(column_name = "organisationRole")]
    pub organisation_role: OrganisationMemberRole,
    #[sea_orm(column_name = "organisationId")]
    pub organisation_id: String,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::organisation::Entity",
        from = "Column::OrganisationId",
        to = "super::organisation::Column::Id",
        on_delete = "Cascade"
    )]
    Organisation,
    #[sea_orm(has_many = "super::organisation_group_member::Entity")]
    OrganisationGroupMembers,
    #[sea_orm(has_many = "super::team_group::Entity")]
    TeamGroups,
}

impl Related<super::organisation::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Organisation.def()
    }
}

impl Related<super::organisation_group_member::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::OrganisationGroupMembers.def()
    }
}

impl Related<super::team_group::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::TeamGroups.def()
    }
}

impl ActiveModelBehavior for ActiveModel {}
