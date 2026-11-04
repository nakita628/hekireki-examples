class PromptHistory < ApplicationRecord
  self.table_name = "prompt_history"

  attribute :modifiedAt, PrismaDateTime.new, default: -> { Time.current }
  alias_attribute :updated_at, :modifiedAt

  validates :prompt, presence: true

  belongs_to :workspace, class_name: "Workspaces", foreign_key: "workspaceId", inverse_of: :prompt_history
  belongs_to :user, class_name: "Users", foreign_key: "modifiedBy", optional: true, inverse_of: :prompt_history
end
