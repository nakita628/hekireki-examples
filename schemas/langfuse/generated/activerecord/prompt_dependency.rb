class PromptDependency < ApplicationRecord
  self.implicit_order_column = "created_at"

  attribute :id, default: -> { Cuid.generate }

  validates :child_name, presence: true

  belongs_to :project, inverse_of: :prompt_dependency
  belongs_to :parent, class_name: "Prompt", inverse_of: :prompt_dependency
end
