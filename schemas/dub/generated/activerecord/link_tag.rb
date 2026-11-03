class LinkTag < ApplicationRecord
  self.table_name = "LinkTag"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :linkId, uniqueness: { scope: :tagId }

  belongs_to :link, foreign_key: "linkId", inverse_of: :tags
  belongs_to :tag, foreign_key: "tagId", inverse_of: :links
end
