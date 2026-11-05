class Member < ApplicationRecord
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  enum :projectScope, { all: "all", selected: "selected" }, default: :all, prefix: true, validate: true

  validates :role, presence: true
  validates :email, presence: true
  validates :organizationId, uniqueness: { scope: :userId, conditions: -> { where.not(userId: nil) } }

  belongs_to :user, foreign_key: "userId", optional: true, inverse_of: :membership
  belongs_to :invited_by, class_name: "User", foreign_key: "invitedById", optional: true, inverse_of: :sent_invites
  belongs_to :organization, foreign_key: "organizationId", inverse_of: :members
  has_many :projects, class_name: "MemberProject", foreign_key: "memberId", inverse_of: :member, dependent: :destroy
end
