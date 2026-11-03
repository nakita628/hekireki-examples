use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "license_validations")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    pub license_id: String,
    pub ip_address: Option<String>,
    pub fingerprint: Option<String>,
    pub validated_at: DateTime,
    pub user_agent: Option<String>,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::license::Entity",
        from = "Column::LicenseId",
        to = "super::license::Column::Id",
        on_delete = "Cascade"
    )]
    License,
}

impl Related<super::license::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::License.def()
    }
}

#[async_trait::async_trait]
impl ActiveModelBehavior for ActiveModel {
    async fn before_save<C>(mut self, _db: &C, insert: bool) -> Result<Self, DbErr>
    where
        C: ConnectionTrait,
    {
        let now = chrono::Utc::now().trunc_subsecs(3);
        if insert && self.validated_at.is_not_set() {
            self.validated_at = Set(now.naive_utc());
        }
        Ok(self)
    }
}
