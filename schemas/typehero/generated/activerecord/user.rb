class User < ApplicationRecord
  self.table_name = "User"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { SecureRandom.uuid }
  attribute :bio, default: ""
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  enum :status, { banned: "BANNED", active: "ACTIVE" }, default: :active, validate: true

  validates :name, presence: true
  validates :email, presence: true, uniqueness: true
  validates :bio, length: { maximum: 1000 }

  has_many :accounts, foreign_key: "userId", inverse_of: :user, dependent: :destroy
  has_many :sessions, foreign_key: "userId", inverse_of: :user, dependent: :destroy
  has_many :challenge, foreign_key: "userId", inverse_of: :user, dependent: :restrict_with_error
  has_many :vote, foreign_key: "userId", inverse_of: :user, dependent: :restrict_with_error
  has_many :submission, foreign_key: "userId", inverse_of: :user, dependent: :restrict_with_error
  has_many :bookmark, foreign_key: "userId", inverse_of: :user, dependent: :restrict_with_error
  has_many :shared_solution, foreign_key: "userId", inverse_of: :user, dependent: :nullify
  has_many :comment, foreign_key: "userId", inverse_of: :user, dependent: :restrict_with_error
  has_many :reports, foreign_key: "reporterId", inverse_of: :reporter, dependent: :restrict_with_error
  has_many :reports_about, class_name: "Report", foreign_key: "userId", inverse_of: :user, dependent: :nullify
  has_many :moderated, class_name: "Report", foreign_key: "moderatorId", inverse_of: :moderator, dependent: :nullify
  has_many :image_upload, foreign_key: "userId", inverse_of: :user, dependent: :restrict_with_error
  has_many :beta_tokens, class_name: "BetaTokens", foreign_key: "userId", inverse_of: :user, dependent: :nullify
  has_many :donations, foreign_key: "userId", inverse_of: :user, dependent: :restrict_with_error
  has_many :short_urls, class_name: "ShortURL", foreign_key: "userId", inverse_of: :user, dependent: :nullify
  has_many :to_user_notification, class_name: "Notification", foreign_key: "toUserId", inverse_of: :to_user,
                                  dependent: :restrict_with_error
  has_many :from_user_notification, class_name: "Notification", foreign_key: "fromUserId", inverse_of: :from_user,
                                    dependent: :restrict_with_error
  has_and_belongs_to_many :roles, join_table: "_RoleToUser", foreign_key: "B", association_foreign_key: "A"
  has_and_belongs_to_many :user_links, join_table: "_UserToUserLink", foreign_key: "A", association_foreign_key: "B"
  has_and_belongs_to_many :tracks, join_table: "_TrackToUser", foreign_key: "B", association_foreign_key: "A"
end
