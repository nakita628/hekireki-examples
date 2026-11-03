class ComputedUpdateOutboxSeed < ApplicationRecord
  self.table_name = "computed_update_outbox_seed"

  attribute :id, default: -> { Cuid.generate }

  validates :table_id, presence: true, uniqueness: { scope: %i[task_id record_id] }
  validates :record_id, presence: true

  belongs_to :task, class_name: "ComputedUpdateOutbox", inverse_of: :seeds
end
