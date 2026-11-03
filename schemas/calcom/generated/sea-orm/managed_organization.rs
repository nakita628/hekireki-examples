use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "ManagedOrganization")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    #[sea_orm(unique)]
    #[sea_orm(column_name = "managedOrganizationId")]
    pub managed_organization_id: i32,
    #[sea_orm(column_name = "managerOrganizationId")]
    pub manager_organization_id: i32,
    #[sea_orm(column_name = "createdAt")]
    pub created_at: DateTime,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::team::Entity",
        from = "Column::ManagedOrganizationId",
        to = "super::team::Column::Id",
        on_delete = "Cascade"
    )]
    ManagedOrganization,
    #[sea_orm(
        belongs_to = "super::team::Entity",
        from = "Column::ManagerOrganizationId",
        to = "super::team::Column::Id",
        on_delete = "Cascade"
    )]
    ManagerOrganization,
}

impl Related<super::team::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::ManagedOrganization.def()
    }
}

#[async_trait::async_trait]
impl ActiveModelBehavior for ActiveModel {
    async fn before_save<C>(mut self, _db: &C, insert: bool) -> Result<Self, DbErr>
    where
        C: ConnectionTrait,
    {
        let now = chrono::Utc::now().trunc_subsecs(3);
        if insert && self.created_at.is_not_set() {
            self.created_at = Set(now.naive_utc());
        }
        Ok(self)
    }
}
