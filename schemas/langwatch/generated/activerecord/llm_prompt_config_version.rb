class LlmPromptConfigVersion < ApplicationRecord
  self.table_name = "LlmPromptConfigVersion"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Nanoid.generate }
  attribute :runtimeParameters, default: -> { JSON.parse("{}") }
  alias_attribute :created_at, :createdAt

  validates :version, presence: true, uniqueness: { scope: :configId }
  validates :config, exclusion: { in: [ nil ] }
  validates :schemaVersion, presence: true
  validates :projectId, presence: true

  belongs_to :author, class_name: "User", foreign_key: "authorId", optional: true,
                      inverse_of: :llm_prompt_config_versions
  belongs_to :config, class_name: "LlmPromptConfig", foreign_key: "configId", inverse_of: :versions
  has_many :version_tags, class_name: "PromptTagAssignment", foreign_key: "versionId", inverse_of: :version,
                          dependent: :destroy
end
