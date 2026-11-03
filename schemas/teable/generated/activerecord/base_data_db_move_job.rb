class BaseDataDbMoveJob < ApplicationRecord
  self.table_name = "base_data_db_move_job"

  attribute :id, default: -> { Cuid.generate }
  alias_attribute :updated_at, :last_modified_time

  enum :state, { pending: "pending", waiting_worker: "waiting_worker", copying_base_schema: "copying_base_schema", copying_shared_rows: "copying_shared_rows", validating: "validating", switching: "switching", succeeded: "succeeded", failed: "failed", cancelled: "cancelled" },
       default: :pending, validate: true

  validates :created_by, presence: true

  belongs_to :base, inverse_of: :data_db_move_jobs
  belongs_to :source_space, class_name: "Space", inverse_of: :base_data_db_move_jobs_as_source
  belongs_to :target_space, class_name: "Space", inverse_of: :base_data_db_move_jobs_as_target
end
