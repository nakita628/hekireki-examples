class User < ApplicationRecord
  self.table_name = "User"

  attribute :uuid, default: -> { SecureRandom.uuid }
  attribute :locale, default: "en"
  attribute :collectionOrder, default: -> { [] }
  attribute :aiPredefinedTags, default: -> { [] }
  attribute :aiTagExistingLinks, default: false
  attribute :readableFontFamily, default: "sans-serif"
  attribute :readableFontSize, default: "20px"
  attribute :readableLineHeight, default: "1.8"
  attribute :readableLineWidth, default: "normal"
  attribute :preventDuplicateLinks, default: false
  attribute :archiveAsScreenshot, default: true
  attribute :archiveAsMonolith, default: true
  attribute :archiveAsPDF, default: true
  attribute :archiveAsReadable, default: true
  attribute :archiveAsWaybackMachine, default: false
  attribute :acceptPromotionalEmails, default: false
  attribute :trialEndEmailSent, default: false
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  enum :linksRouteTo, { original: "ORIGINAL", pdf: "PDF", readable: "READABLE", monolith: "MONOLITH", screenshot: "SCREENSHOT", details: "DETAILS" },
       default: :original, validate: true
  enum :aiTaggingMethod, { disabled: "DISABLED", generate: "GENERATE", existing: "EXISTING", predefined: "PREDEFINED" },
       default: :disabled, validate: true
  enum :theme, { dark: "dark", light: "light", auto: "auto" }, default: :dark, validate: true

  validates :uuid, uniqueness: true
  validates :username, uniqueness: { allow_nil: true }
  validates :email, uniqueness: { allow_nil: true }

  belongs_to :parent_subscription, class_name: "Subscription", foreign_key: "parentSubscriptionId", optional: true,
                                   inverse_of: :child_users
  has_one :subscriptions, class_name: "Subscription", foreign_key: "userId", inverse_of: :user, dependent: :destroy
  has_many :accounts, foreign_key: "userId", inverse_of: :user, dependent: :destroy
  has_many :collections, foreign_key: "ownerId", inverse_of: :owner, dependent: :destroy
  has_many :tags, foreign_key: "ownerId", inverse_of: :owner, dependent: :destroy
  has_many :created_links, class_name: "Link", foreign_key: "createdById", inverse_of: :created_by, dependent: :destroy
  has_many :created_collections, class_name: "Collection", foreign_key: "createdById", inverse_of: :created_by,
                                 dependent: :nullify
  has_many :highlights, foreign_key: "userId", inverse_of: :user, dependent: :destroy
  has_many :collections_joined, class_name: "UsersAndCollections", foreign_key: "userId", inverse_of: :user,
                                dependent: :destroy
  has_many :access_tokens, foreign_key: "userId", inverse_of: :user, dependent: :destroy
  has_many :dashboard_sections, foreign_key: "userId", inverse_of: :user, dependent: :destroy
  has_and_belongs_to_many :pinned_links, class_name: "Link", join_table: "_PinnedLinks", foreign_key: "B",
                                         association_foreign_key: "A"
end
