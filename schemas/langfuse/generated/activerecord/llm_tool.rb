class LlmTool < ApplicationRecord
  self.implicit_order_column = "created_at"

  attribute :id, default: -> { Cuid.generate }

  validates :name, presence: true, uniqueness: { scope: :project_id }
  validates :description, presence: true
  validates :parameters, exclusion: { in: [ nil ] }

  belongs_to :project, inverse_of: :llm_tool
end
