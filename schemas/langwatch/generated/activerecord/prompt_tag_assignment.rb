class PromptTagAssignment < ApplicationRecord
  self.table_name = "PromptTagAssignment"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Nanoid.generate }
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :configId, uniqueness: { scope: :tagId }
  validates :projectId, presence: true

  belongs_to :config, class_name: "LlmPromptConfig", foreign_key: "configId", inverse_of: :version_tags
  belongs_to :version, class_name: "LlmPromptConfigVersion", foreign_key: "versionId", inverse_of: :version_tags
  belongs_to :prompt_tag, foreign_key: "tagId", inverse_of: :assignments
  belongs_to :created_by, class_name: "User", foreign_key: "createdById", optional: true,
                          inverse_of: :version_tags_created
  belongs_to :updated_by, class_name: "User", foreign_key: "updatedById", optional: true,
                          inverse_of: :version_tags_updated
end
