class LlmPromptConfig < ApplicationRecord
  self.table_name = "LlmPromptConfig"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Nanoid.generate }
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  enum :scope, { organization: "ORGANIZATION", project: "PROJECT" }, default: :project, validate: true

  validates :handle, uniqueness: { allow_nil: true }
  validates :handle, uniqueness: { allow_nil: true }
  validates :name, presence: true
  validates :organizationId, presence: true

  belongs_to :project, foreign_key: "projectId", inverse_of: :llm_prompt_configs
  belongs_to :copied_from, class_name: "LlmPromptConfig", foreign_key: "copiedFromPromptId", optional: true,
                           inverse_of: :copied_prompts
  has_many :copied_prompts, class_name: "LlmPromptConfig", foreign_key: "copiedFromPromptId", inverse_of: :copied_from,
                            dependent: :restrict_with_error
  has_many :versions, class_name: "LlmPromptConfigVersion", foreign_key: "configId", inverse_of: :config,
                      dependent: :destroy
  has_many :version_tags, class_name: "PromptTagAssignment", foreign_key: "configId", inverse_of: :config,
                          dependent: :destroy
end
