class User < ApplicationRecord
  attribute :emailVerified, default: false
  attribute :twoFactorEnabled, default: false
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :email, presence: true, uniqueness: true

  has_one :two_factor, foreign_key: "userId", inverse_of: :user, dependent: :destroy
  has_one :totp, class_name: "UserTotp", foreign_key: "userId", inverse_of: :user, dependent: :destroy
  has_many :created_organizations, class_name: "Organization", foreign_key: "createdByUserId", inverse_of: :created_by,
                                   dependent: :nullify
  has_many :subscriptions, class_name: "Organization", foreign_key: "subscriptionCreatedByUserId",
                           inverse_of: :subscription_created_by, dependent: :nullify
  has_many :membership, class_name: "Member", foreign_key: "userId", inverse_of: :user, dependent: :destroy
  has_many :sent_invites, class_name: "Member", foreign_key: "invitedById", inverse_of: :invited_by, dependent: :nullify
  has_many :sessions, foreign_key: "userId", inverse_of: :user, dependent: :destroy
  has_many :accounts, foreign_key: "userId", inverse_of: :user, dependent: :destroy
  has_many :invites, foreign_key: "createdById", inverse_of: :created_by, dependent: :destroy
  has_many :conversations, foreign_key: "userId", inverse_of: :user, dependent: :destroy
  has_many :two_factor_challenges, foreign_key: "userId", inverse_of: :user, dependent: :destroy
end
