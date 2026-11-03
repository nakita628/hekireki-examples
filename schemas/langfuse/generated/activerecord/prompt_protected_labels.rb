class PromptProtectedLabels < ApplicationRecord
  self.implicit_order_column = "created_at"

  attribute :id, default: -> { Cuid.generate }

  validates :label, presence: true, uniqueness: { scope: :project_id }

  belongs_to :project, inverse_of: :prompt_protected_labels
end
