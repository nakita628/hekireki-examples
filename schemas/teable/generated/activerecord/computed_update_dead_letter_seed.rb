class ComputedUpdateDeadLetterSeed < ApplicationRecord
  self.table_name = "computed_update_dead_letter_seed"
  self.primary_key = %w[task_id table_id record_id]

  belongs_to :task, class_name: "ComputedUpdateDeadLetter", inverse_of: :seeds
end
