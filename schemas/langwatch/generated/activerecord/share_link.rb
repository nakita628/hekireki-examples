class ShareLink < ApplicationRecord
  self.table_name = "ShareLink"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Nanoid.generate }
  attribute :viewCount, default: 0
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  enum :resourceType, { trace: "TRACE", thread: "THREAD" }, validate: true
  enum :visibility, { public: "PUBLIC", organization: "ORGANIZATION", project: "PROJECT" }, default: :public,
                                                                                            prefix: true, validate: true

  validates :token, presence: true, uniqueness: true
  validates :resourceId, presence: true

  belongs_to :project, foreign_key: "projectId", inverse_of: :share_links
  belongs_to :user, foreign_key: "userId", optional: true, inverse_of: :share_links
end
