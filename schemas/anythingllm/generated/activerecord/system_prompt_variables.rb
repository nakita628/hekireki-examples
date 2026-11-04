class SystemPromptVariables < ApplicationRecord
  self.inheritance_column = nil

  attribute :type, default: "system"
  attribute :createdAt, PrismaDateTime.new, default: -> { Time.current }
  attribute :updatedAt, PrismaDateTime.new
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :key, presence: true, uniqueness: true

  belongs_to :user, class_name: "Users", foreign_key: "userId", optional: true, inverse_of: :system_prompt_variables
end
