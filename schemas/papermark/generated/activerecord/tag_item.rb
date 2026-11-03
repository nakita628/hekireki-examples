class TagItem < ApplicationRecord
  self.table_name = "TagItem"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  enum :itemType, { link_tag: "LINK_TAG", document_tag: "DOCUMENT_TAG", dataroom_tag: "DATAROOM_TAG" }, validate: true

  belongs_to :tag, foreign_key: "tagId", inverse_of: :items
  belongs_to :link, foreign_key: "linkId", optional: true, inverse_of: :tags
  belongs_to :document, foreign_key: "documentId", optional: true, inverse_of: :tags
  belongs_to :dataroom, foreign_key: "dataroomId", optional: true, inverse_of: :tags
end
