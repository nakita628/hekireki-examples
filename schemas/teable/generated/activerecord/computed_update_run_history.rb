class ComputedUpdateRunHistory < ApplicationRecord
  self.table_name = "computed_update_run_history"
  self.primary_key = "task_id"

  attribute :origin_run_ids, default: -> { [] }
  attribute :affected_table_ids, default: -> { [] }
  attribute :affected_field_ids, default: -> { [] }
  attribute :source_field_ids, default: -> { [] }
  attribute :seed_record_count, default: 0
  attribute :stage_depth, default: 0
  attribute :run_total_steps, default: 0
  attribute :run_completed_steps_before, default: 0
  attribute :estimated_complexity, default: 0
  attribute :attempts, default: 0
  attribute :duration_ms, default: 0

  validates :base_id, presence: true
  validates :seed_table_id, presence: true
  validates :change_type, presence: true
  validates :run_id, presence: true
  validates :outcome, presence: true
  validates :enqueued_at, presence: true
  validates :completed_at, presence: true
end
