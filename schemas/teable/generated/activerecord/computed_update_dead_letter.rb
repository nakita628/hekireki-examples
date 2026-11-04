class ComputedUpdateDeadLetter < ApplicationRecord
  self.table_name = "computed_update_dead_letter"

  attribute :worker_lane, default: "production"
  attribute :attempts, default: 0
  attribute :max_attempts, default: 8
  attribute :estimated_complexity, default: 0
  attribute :origin_run_ids, default: -> { [] }
  attribute :run_total_steps, default: 0
  attribute :run_completed_steps_before, default: 0
  attribute :affected_table_ids, default: -> { [] }
  attribute :affected_field_ids, default: -> { [] }
  attribute :stage_depth, default: 0

  validates :base_id, presence: true
  validates :seed_table_id, presence: true
  validates :change_type, presence: true
  validates :status, presence: true
  validates :next_run_at, presence: true
  validates :plan_hash, presence: true
  validates :run_id, presence: true
  validates :failed_at, presence: true

  has_many :seeds, class_name: "ComputedUpdateDeadLetterSeed", foreign_key: "task_id", inverse_of: :task,
                   dependent: :destroy
end
