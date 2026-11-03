class Comments < ApplicationRecord
  self.table_name = "Comments"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { SecureRandom.uuid }
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :content, presence: true

  belongs_to :organization, foreign_key: "organizationId", inverse_of: :comments
  belongs_to :post, foreign_key: "postId", inverse_of: :comments
  belongs_to :user, foreign_key: "userId", optional: true, inverse_of: :comments
  belongs_to :parent, class_name: "Comments", foreign_key: "parentId", optional: true, inverse_of: :replies
  has_many :replies, class_name: "Comments", foreign_key: "parentId", inverse_of: :parent, dependent: :nullify
end
