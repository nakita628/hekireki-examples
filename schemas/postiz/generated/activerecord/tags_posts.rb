class TagsPosts < ApplicationRecord
  self.table_name = "TagsPosts"
  self.primary_key = %w[postId tagId]

  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :postId, uniqueness: { scope: :tagId }

  belongs_to :post, foreign_key: "postId", inverse_of: :tags
  belongs_to :tag, class_name: "Tags", foreign_key: "tagId", inverse_of: :posts
end
