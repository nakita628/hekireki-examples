class PromptTag < ApplicationRecord
  self.table_name = "PromptTag"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Nanoid.generate }
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :name, presence: true, uniqueness: { scope: :organizationId }

  belongs_to :organization, foreign_key: "organizationId", inverse_of: :prompt_tags
  belongs_to :created_by, class_name: "User", foreign_key: "createdById", optional: true,
                          inverse_of: :prompt_tags_created
  belongs_to :updated_by, class_name: "User", foreign_key: "updatedById", optional: true,
                          inverse_of: :prompt_tags_updated
  has_many :assignments, class_name: "PromptTagAssignment", foreign_key: "tagId", inverse_of: :prompt_tag,
                         dependent: :destroy
end
