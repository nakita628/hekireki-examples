class LiteLLMSSOIdentityAssertion < ApplicationRecord
  self.table_name = "LiteLLM_SSOIdentityAssertion"
  self.primary_key = "user_id"

  validates :assertion_b64, presence: true
end
