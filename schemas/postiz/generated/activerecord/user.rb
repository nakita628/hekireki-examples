class User < ApplicationRecord
  self.table_name = "User"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { SecureRandom.uuid }
  attribute :isSuperAdmin, default: false
  attribute :audience, default: 0
  attribute :activated, default: true
  attribute :connectedAccount, default: false
  attribute :sendSuccessEmails, default: true
  attribute :sendFailureEmails, default: true
  attribute :sendStreakEmails, default: true
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  enum :providerName, { local: "LOCAL", github: "GITHUB", google: "GOOGLE", apple: "APPLE", farcaster: "FARCASTER", wallet: "WALLET", generic: "GENERIC" },
       validate: true

  validates :email, presence: true, uniqueness: { scope: :providerName }
  validates :timezone, presence: true

  belongs_to :picture, class_name: "Media", foreign_key: "pictureId", optional: true, inverse_of: :user_picture
  has_one :agencies, class_name: "SocialMediaAgency", foreign_key: "userId", inverse_of: :user,
                     dependent: :restrict_with_error
  has_many :comments, class_name: "Comments", foreign_key: "userId", inverse_of: :user, dependent: :nullify
  has_many :items, class_name: "ItemUser", foreign_key: "userId", inverse_of: :user, dependent: :restrict_with_error
  has_many :group_buyer, class_name: "MessagesGroup", foreign_key: "buyerId", inverse_of: :buyer,
                         dependent: :restrict_with_error
  has_many :group_seller, class_name: "MessagesGroup", foreign_key: "sellerId", inverse_of: :seller,
                          dependent: :restrict_with_error
  has_many :order_buyer, class_name: "Orders", foreign_key: "buyerId", inverse_of: :buyer,
                         dependent: :restrict_with_error
  has_many :order_seller, class_name: "Orders", foreign_key: "sellerId", inverse_of: :seller,
                          dependent: :restrict_with_error
  has_many :payout_problems, class_name: "PayoutProblems", foreign_key: "userId", inverse_of: :user,
                             dependent: :restrict_with_error
  has_many :organizations, class_name: "UserOrganization", foreign_key: "userId", inverse_of: :user,
                           dependent: :restrict_with_error
  has_many :oauth_authorizations, class_name: "OAuthAuthorization", foreign_key: "userId", inverse_of: :user,
                                  dependent: :restrict_with_error
end
