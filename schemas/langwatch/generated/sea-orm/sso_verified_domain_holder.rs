use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "SsoVerifiedDomainHolder")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub domain: String,
    #[sea_orm(primary_key, auto_increment = false)]
    #[sea_orm(column_name = "connectionId")]
    pub connection_id: String,
    #[sea_orm(column_name = "organizationId")]
    pub organization_id: String,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::sso_verified_domain::Entity",
        from = "(Column::Domain, Column::OrganizationId)",
        to = "(super::sso_verified_domain::Column::Domain, super::sso_verified_domain::Column::OrganizationId)",
        on_delete = "Cascade"
    )]
    Ownership,
}

impl Related<super::sso_verified_domain::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Ownership.def()
    }
}

impl ActiveModelBehavior for ActiveModel {}
