class MockServer < ApplicationRecord
  self.table_name = "MockServer"

  attribute :id, default: -> { Cuid.generate }
  attribute :delayInMs, default: 0
  attribute :isPublic, default: true
  attribute :isActive, default: true
  attribute :hitCount, default: 0
  alias_attribute :updated_at, :updatedOn

  enum :workspaceType, { user: "USER", team: "TEAM" }, validate: true

  validates :name, presence: true
  validates :subdomain, presence: true, uniqueness: true
  validates :collectionID, presence: true
  validates :workspaceID, presence: true

  belongs_to :user, foreign_key: "creatorUid", optional: true, inverse_of: :mock_servers
  has_many :request_logs, class_name: "MockServerLog", foreign_key: "mockServerID", inverse_of: :mock_server,
                          dependent: :destroy
  has_many :activity_history, class_name: "MockServerActivity", foreign_key: "mockServerID", inverse_of: :mock_server,
                              dependent: :destroy
end
