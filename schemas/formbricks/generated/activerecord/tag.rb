class Tag < ApplicationRecord
  self.table_name = "Tag"
  self.implicit_order_column = "created_at"

  attribute :id, default: -> { Cuid.generate }

  validates :name, presence: true, uniqueness: { scope: :workspaceId }

  belongs_to :workspace, foreign_key: "workspaceId", inverse_of: :tags
  has_many :responses, class_name: "TagsOnResponses", foreign_key: "tagId", inverse_of: :tag, dependent: :destroy
end
