class ScimSubject < ApplicationRecord
  self.table_name = "scimSubject"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :userId, uniqueness: true
  validates :userId, uniqueness: true
  validates :revision, presence: true

  belongs_to :user, foreign_key: "userId", inverse_of: :scim_subject
end
