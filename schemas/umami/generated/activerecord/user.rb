class User < ApplicationRecord
  self.table_name = "user"
  self.primary_key = "user_id"

  attribute :two_factor_required, default: false

  validates :username, presence: true, length: { maximum: 255 }, uniqueness: true
  validates :password, presence: true, length: { maximum: 60 }
  validates :role, presence: true, length: { maximum: 50 }
  validates :logo_url, length: { maximum: 2183 }
  validates :display_name, length: { maximum: 255 }

  has_one :two_factor_auth, dependent: :destroy
  has_one :two_factor_rate_limit, dependent: :destroy
  has_many :websites, dependent: :nullify
  has_many :created_by, class_name: "Website", foreign_key: "created_by", inverse_of: :create_user, dependent: :nullify
  has_many :links, dependent: :nullify
  has_many :pixels, dependent: :nullify
  has_many :teams, class_name: "TeamUser", inverse_of: :user, dependent: :restrict_with_error
  has_many :reports, dependent: :restrict_with_error
  has_many :boards, dependent: :nullify
  has_many :two_factor_backup_codes, dependent: :destroy
  has_many :two_factor_otp_useds, dependent: :destroy
  has_many :api_keys, dependent: :destroy
end
