class User < ApplicationRecord
  self.table_name = "User"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { SecureRandom.uuid }
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  enum :provider, { anonymous: "ANONYMOUS", google: "GOOGLE", internet_identity: "INTERNET_IDENTITY", oidc: "OIDC" },
       default: :anonymous, validate: true
  enum :role, { admin: "ADMIN", demo: "DEMO", inactive: "INACTIVE", user: "USER" }, default: :user, validate: true

  has_one :analytics, foreign_key: "userId", inverse_of: :user, dependent: :destroy
  has_one :settings, foreign_key: "userId", inverse_of: :user, dependent: :destroy
  has_many :accesses_get, class_name: "Access", foreign_key: "granteeUserId", inverse_of: :grantee_user,
                          dependent: :destroy
  has_many :accesses_give, class_name: "Access", foreign_key: "userId", inverse_of: :user, dependent: :destroy
  has_many :accounts, foreign_key: "userId", inverse_of: :user, dependent: :destroy
  has_many :activities, class_name: "Order", foreign_key: "userId", inverse_of: :user, dependent: :destroy
  has_many :api_keys, foreign_key: "userId", inverse_of: :user, dependent: :destroy
  has_many :auth_devices, foreign_key: "userId", inverse_of: :user, dependent: :destroy
  has_many :subscriptions, foreign_key: "userId", inverse_of: :user, dependent: :destroy
  has_many :tags, foreign_key: "userId", inverse_of: :user, dependent: :destroy
  has_many :symbol_profile, foreign_key: "userId", inverse_of: :user, dependent: :destroy
  has_and_belongs_to_many :watchlist, class_name: "SymbolProfile", join_table: "_UserWatchlist", foreign_key: "B",
                                      association_foreign_key: "A"
end
