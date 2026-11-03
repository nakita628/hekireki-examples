class Space < ApplicationRecord
  self.table_name = "space"

  attribute :id, default: -> { Cuid.generate }
  alias_attribute :updated_at, :last_modified_time

  validates :name, presence: true
  validates :created_by, presence: true

  has_one :data_db_binding, class_name: "SpaceDataDbBinding", inverse_of: :space, dependent: :destroy
  has_many :base_group, class_name: "Base", inverse_of: :space, dependent: :restrict_with_error
  has_many :data_db_migration_jobs, class_name: "SpaceDataDbMigrationJob", inverse_of: :space, dependent: :destroy
  has_many :base_data_db_move_jobs_as_source, class_name: "BaseDataDbMoveJob", foreign_key: "source_space_id",
                                              inverse_of: :source_space, dependent: :destroy
  has_many :base_data_db_move_jobs_as_target, class_name: "BaseDataDbMoveJob", foreign_key: "target_space_id",
                                              inverse_of: :target_space, dependent: :destroy
end
