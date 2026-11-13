class CaptureAuth < ApplicationRecord
  self.table_name = "capture_auth"

  validates :client_id, presence: true
  validates :client_name, presence: true
  validates :redirect_uri, presence: true
  validates :code_challenge, presence: true
  validates :resource, presence: true
  validates :state, presence: true
  validates :scope, presence: true
  validates :code_hash, uniqueness: { allow_nil: true }
  validates :expires_at, presence: true

  belongs_to :user, optional: true, inverse_of: :capture_authorizations
end
