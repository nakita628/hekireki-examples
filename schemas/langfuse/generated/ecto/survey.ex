defmodule Langfuse.Survey do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Langfuse.PrismaDateTime, autogenerate: {Langfuse.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          survey_name: atom(),
          response: map(),
          user_email: String.t() | nil,
          inserted_at: DateTime.t(),
          org: Langfuse.Organization.t() | nil,
          user: Langfuse.User.t() | nil
        }

  schema "surveys" do
    field(:id, :string, primary_key: true)
    field(:survey_name, Ecto.Enum, values: [ORG_ONBOARDING: "org_onboarding", USER_ONBOARDING: "user_onboarding"])
    field(:response, :map)
    field(:user_email, :string)
    belongs_to(:org, Langfuse.Organization, foreign_key: :org_id, type: :string)
    belongs_to(:user, Langfuse.User, foreign_key: :user_id, type: :string)
    timestamps(inserted_at_source: :created_at, updated_at: false)
  end
end