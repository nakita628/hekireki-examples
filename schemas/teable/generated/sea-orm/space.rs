use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "space")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    pub name: String,
    pub avatar: Option<String>,
    pub credit: Option<i32>,
    pub deleted_time: Option<DateTime>,
    pub created_time: DateTime,
    pub created_by: String,
    pub last_modified_by: Option<String>,
    pub last_modified_time: Option<DateTime>,
    pub is_template: Option<bool>,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(has_many = "super::base::Entity")]
    BaseGroup,
    #[sea_orm(has_many = "super::space_data_db_migration_job::Entity")]
    DataDbMigrationJobs,
    #[sea_orm(
        has_many = "super::base_data_db_move_job::Entity",
        from = "Column::Id",
        to = "super::base_data_db_move_job::Column::SourceSpaceId"
    )]
    BaseDataDbMoveJobsAsSource,
    #[sea_orm(
        has_many = "super::base_data_db_move_job::Entity",
        from = "Column::Id",
        to = "super::base_data_db_move_job::Column::TargetSpaceId"
    )]
    BaseDataDbMoveJobsAsTarget,
    #[sea_orm(has_one = "super::space_data_db_binding::Entity")]
    DataDbBinding,
}

impl Related<super::base::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::BaseGroup.def()
    }
}

impl Related<super::space_data_db_migration_job::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::DataDbMigrationJobs.def()
    }
}

impl Related<super::base_data_db_move_job::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::BaseDataDbMoveJobsAsSource.def()
    }
}

impl Related<super::space_data_db_binding::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::DataDbBinding.def()
    }
}

#[async_trait::async_trait]
impl ActiveModelBehavior for ActiveModel {
    async fn before_save<C>(mut self, _db: &C, insert: bool) -> Result<Self, DbErr>
    where
        C: ConnectionTrait,
    {
        let now = chrono::Utc::now().trunc_subsecs(3);
        if insert && self.created_time.is_not_set() {
            self.created_time = Set(now.naive_utc());
        }
        if !self.last_modified_time.is_set() {
            self.last_modified_time = Set(Some(now.naive_utc()));
        }
        Ok(self)
    }
}
