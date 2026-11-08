class ApiRequest < ApplicationRecord
  alias_attribute :created_at, :createdAt

  validates :method, presence: true
  validates :path, presence: true
  validates :statusCode, presence: true
  validates :duration, presence: true

  belongs_to :project, foreign_key: "projectId", optional: true, inverse_of: :api_requests
end
