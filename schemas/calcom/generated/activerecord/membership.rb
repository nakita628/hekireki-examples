class Membership < ApplicationRecord
  self.table_name = "Membership"

  attribute :accepted, default: false
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  enum :role, { member: "MEMBER", admin: "ADMIN", owner: "OWNER" }, validate: true

  validates :userId, uniqueness: { scope: :teamId }

  belongs_to :custom_role, class_name: "Role", foreign_key: "customRoleId", optional: true, inverse_of: :memberships
  belongs_to :team, foreign_key: "teamId", inverse_of: :members
  belongs_to :user, foreign_key: "userId", inverse_of: :teams
  has_many :attribute_to_user, class_name: "AttributeToUser", foreign_key: "memberId", inverse_of: :member,
                               dependent: :destroy
  has_many :host, class_name: "Host", foreign_key: "memberId", inverse_of: :member, dependent: :destroy
end
