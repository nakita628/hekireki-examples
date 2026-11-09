class UserProfile < ApplicationRecord
  self.table_name = "UserProfile"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  attribute :admin, default: false
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :name, presence: true
  validates :email, presence: true
  validates :loginProvider, presence: true, uniqueness: { scope: :externalId }
  validates :externalId, presence: true

  has_one :password, class_name: "UserPassword", foreign_key: "userId", inverse_of: :user,
                     dependent: :restrict_with_error
  has_many :tokens, class_name: "UserApiToken", foreign_key: "userId", inverse_of: :user,
                    dependent: :restrict_with_error
  has_many :workspace_access, foreign_key: "userId", inverse_of: :user, dependent: :restrict_with_error
  has_many :user_preferences, class_name: "UserPreferences", foreign_key: "userId", inverse_of: :user,
                              dependent: :restrict_with_error
  has_many :workspace_user_properties, class_name: "WorkspaceUserProperties", foreign_key: "userId", inverse_of: :user,
                                       dependent: :restrict_with_error
end
