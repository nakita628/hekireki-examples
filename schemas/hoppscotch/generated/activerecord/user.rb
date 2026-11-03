class User < ApplicationRecord
  self.table_name = "User"
  self.primary_key = "uid"

  attribute :uid, default: -> { Cuid.generate }
  attribute :isAdmin, default: false

  validates :email, uniqueness: { allow_nil: true }

  has_one :settings, class_name: "UserSettings", foreign_key: "userUid", inverse_of: :user, dependent: :destroy
  has_many :provider_accounts, class_name: "Account", foreign_key: "userId", inverse_of: :user, dependent: :destroy
  has_many :invited_users, class_name: "InvitedUsers", foreign_key: "adminUid", inverse_of: :user, dependent: :destroy
  has_many :mock_servers, foreign_key: "creatorUid", inverse_of: :user, dependent: :nullify
  has_many :personal_access_tokens, foreign_key: "userUid", inverse_of: :user, dependent: :destroy
  has_many :shortcodes, foreign_key: "creatorUid", inverse_of: :user, dependent: :nullify
  has_many :user_collections, foreign_key: "userUid", inverse_of: :user, dependent: :destroy
  has_many :environments, class_name: "UserEnvironment", foreign_key: "userUid", inverse_of: :user, dependent: :destroy
  has_many :history, class_name: "UserHistory", foreign_key: "userUid", inverse_of: :user, dependent: :destroy
  has_many :user_requests, foreign_key: "userUid", inverse_of: :user, dependent: :destroy
  has_many :verification_tokens, foreign_key: "userUid", inverse_of: :user, dependent: :destroy
end
