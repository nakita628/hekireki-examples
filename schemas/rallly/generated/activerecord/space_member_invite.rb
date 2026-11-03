class SpaceMemberInvite < ApplicationRecord
  self.implicit_order_column = "created_at"

  attribute :id, default: -> { SecureRandom.uuid }

  enum :role, { admin: "ADMIN", member: "MEMBER" }, default: :member, validate: true

  validates :email, presence: true, uniqueness: { scope: :space_id }

  belongs_to :invited_by, class_name: "User", foreign_key: "inviter_id", inverse_of: :space_member_invites
  belongs_to :space, inverse_of: :member_invites
end
