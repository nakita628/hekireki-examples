class GscConnection < ApplicationRecord
  attribute :siteUrl, default: ""
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :projectId, uniqueness: true
  validates :accessToken, presence: true
  validates :refreshToken, presence: true

  belongs_to :project, foreign_key: "projectId", inverse_of: :gsc_connection
end
