class User < ApplicationRecord
  self.table_name = "User"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  enum :graphNavigation, { mouse: "MOUSE", trackpad: "TRACKPAD" }, validate: { allow_nil: true }

  validates :email, uniqueness: { allow_nil: true }
  validates :onboardingCategories, exclusion: { in: [ nil ] }

  has_many :accounts, foreign_key: "userId", inverse_of: :user, dependent: :destroy
  has_many :api_tokens, foreign_key: "ownerId", inverse_of: :owner, dependent: :destroy
  has_many :collaborators_on_typebots, foreign_key: "userId", inverse_of: :user, dependent: :destroy
  has_many :workspaces, class_name: "MemberInWorkspace", foreign_key: "userId", inverse_of: :user, dependent: :destroy
  has_many :sessions, foreign_key: "userId", inverse_of: :user, dependent: :destroy
  has_many :banned_ips, foreign_key: "userId", inverse_of: :user, dependent: :restrict_with_error
  has_many :credentials, class_name: "UserCredentials", foreign_key: "userId", inverse_of: :user, dependent: :destroy
end
