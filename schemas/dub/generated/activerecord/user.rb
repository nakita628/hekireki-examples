class User < ApplicationRecord
  self.table_name = "User"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  attribute :isMachine, default: false
  attribute :invalidLoginAttempts, default: 0
  attribute :sentMail, default: false
  alias_attribute :created_at, :createdAt

  validates :email, uniqueness: { allow_nil: true }

  has_one :notification_preferences, class_name: "UserNotificationPreferences", foreign_key: "userId", inverse_of: :user,
                                     dependent: :destroy
  has_many :accounts, foreign_key: "userId", inverse_of: :user, dependent: :destroy
  has_many :sessions, foreign_key: "userId", inverse_of: :user, dependent: :destroy
  has_many :projects, class_name: "ProjectUsers", foreign_key: "userId", inverse_of: :user, dependent: :destroy
  has_many :links, foreign_key: "userId", inverse_of: :user, dependent: :nullify
  has_many :dashboards, foreign_key: "userId", inverse_of: :user, dependent: :destroy
  has_many :tokens, foreign_key: "userId", inverse_of: :user, dependent: :destroy
  has_many :restricted_tokens, foreign_key: "userId", inverse_of: :user, dependent: :destroy
  has_many :o_auth_codes, foreign_key: "userId", inverse_of: :user, dependent: :destroy
  has_many :integrations, foreign_key: "userId", inverse_of: :user, dependent: :nullify
  has_many :installed_integrations, foreign_key: "userId", inverse_of: :user, dependent: :destroy
  has_many :folders, class_name: "FolderUser", foreign_key: "userId", inverse_of: :user, dependent: :destroy
  has_many :folder_access_requests, foreign_key: "userId", inverse_of: :user, dependent: :destroy
  has_many :utm_templates, foreign_key: "userId", inverse_of: :user, dependent: :nullify
end
