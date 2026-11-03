class SpaceMember < ApplicationRecord
  self.implicit_order_column = "created_at"

  attribute :id, default: -> { SecureRandom.uuid }

  enum :role, { admin: "ADMIN", member: "MEMBER" }, default: :member, validate: true

  validates :space_id, uniqueness: { scope: :user_id }

  belongs_to :space, inverse_of: :members
  belongs_to :user, inverse_of: :member_of
end
