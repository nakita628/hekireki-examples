class Tag < ApplicationRecord
  self.table_name = "Tag"

  attribute :color, default: "blue"
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :name, presence: true, uniqueness: { scope: :projectId, conditions: -> { where.not(projectId: nil) } }

  belongs_to :project, foreign_key: "projectId", optional: true, inverse_of: :tags
  has_many :links, class_name: "LinkTag", foreign_key: "tagId", inverse_of: :tag, dependent: :destroy
end
