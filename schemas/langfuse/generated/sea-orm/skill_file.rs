use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "skill_files")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    pub created_at: DateTime,
    pub project_id: String,
    pub skill_id: String,
    pub blob_id: String,
    pub path: String,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::project::Entity",
        from = "Column::ProjectId",
        to = "super::project::Column::Id",
        on_delete = "Cascade"
    )]
    Project,
    #[sea_orm(
        belongs_to = "super::skill::Entity",
        from = "Column::SkillId",
        to = "super::skill::Column::Id",
        on_delete = "Cascade"
    )]
    Skill,
    #[sea_orm(
        belongs_to = "super::skill_blob::Entity",
        from = "Column::BlobId",
        to = "super::skill_blob::Column::Id",
        on_delete = "Restrict"
    )]
    Blob,
}

impl Related<super::project::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Project.def()
    }
}

impl Related<super::skill::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Skill.def()
    }
}

impl Related<super::skill_blob::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Blob.def()
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
