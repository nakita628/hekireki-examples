use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "SsoVerifiedDomain")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub domain: String,
    #[sea_orm(column_name = "organizationId")]
    pub organization_id: String,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(has_many = "super::sso_verified_domain_holder::Entity")]
    Holders,
}

impl Related<super::sso_verified_domain_holder::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Holders.def()
    }
}

impl ActiveModelBehavior for ActiveModel {}
