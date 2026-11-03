class Post < ApplicationRecord
  self.table_name = "Post"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  attribute :delay, default: 0
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  enum :state, { queue: "QUEUE", published: "PUBLISHED", error: "ERROR", draft: "DRAFT" }, default: :queue,
                                                                                           validate: true
  enum :approvedSubmitForOrder, { no: "NO", waiting_confirmation: "WAITING_CONFIRMATION", yes: "YES" }, default: :no,
                                                                                                        validate: true
  enum :creationMethod, { unknown: "UNKNOWN", web: "WEB", mcp: "MCP", api: "API", autopost: "AUTOPOST", cli: "CLI" },
       default: :unknown, validate: true

  validates :publishDate, presence: true
  validates :content, presence: true
  validates :group, presence: true

  belongs_to :integration, foreign_key: "integrationId", inverse_of: :posts
  belongs_to :last_message, class_name: "Messages", foreign_key: "lastMessageId", optional: true, inverse_of: :posts
  belongs_to :organization, foreign_key: "organizationId", inverse_of: :post
  belongs_to :parent_post, class_name: "Post", foreign_key: "parentPostId", optional: true, inverse_of: :children_post
  belongs_to :submitted_for_order, class_name: "Orders", foreign_key: "submittedForOrderId", optional: true,
                                   inverse_of: :posts
  belongs_to :submitted_for_organization, class_name: "Organization", foreign_key: "submittedForOrganizationId",
                                          optional: true, inverse_of: :submitted_post
  has_many :comments, foreign_key: "postId", inverse_of: :post, dependent: :restrict_with_error
  has_many :errors, foreign_key: "postId", inverse_of: :post, dependent: :restrict_with_error
  has_many :payout_problems, foreign_key: "postId", inverse_of: :post, dependent: :nullify
  has_many :children_post, class_name: "Post", foreign_key: "parentPostId", inverse_of: :parent_post,
                           dependent: :nullify
  has_many :tags, class_name: "TagsPosts", foreign_key: "postId", inverse_of: :post, dependent: :restrict_with_error
end
