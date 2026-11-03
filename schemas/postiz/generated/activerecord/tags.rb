class Tags < ApplicationRecord
  self.table_name = "Tags"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { SecureRandom.uuid }
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :name, presence: true
  validates :color, presence: true

  belongs_to :organization, foreign_key: "orgId", inverse_of: :tags
  has_many :posts, class_name: "TagsPosts", foreign_key: "tagId", inverse_of: :tag, dependent: :restrict_with_error
end
