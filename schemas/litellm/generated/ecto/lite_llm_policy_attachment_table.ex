defmodule LiteLLM.LiteLLMPolicyAttachmentTable do
  use Ecto.Schema

  @primary_key {:id, :binary_id, autogenerate: true, source: :attachment_id}
  @foreign_key_type :binary_id
  @timestamps_opts [type: LiteLLM.PrismaDateTime, autogenerate: {LiteLLM.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: Ecto.UUID.t(),
          policy_name: String.t(),
          scope: String.t() | nil,
          teams: [String.t()],
          keys: [String.t()],
          models: [String.t()],
          tags: [String.t()],
          priority: integer() | nil,
          is_default: boolean(),
          created_by: String.t() | nil,
          updated_by: String.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t()
        }

  schema "LiteLLM_PolicyAttachmentTable" do
    field(:policy_name, :string)
    field(:scope, :string)
    field(:teams, {:array, :string})
    field(:keys, {:array, :string})
    field(:models, {:array, :string})
    field(:tags, {:array, :string})
    field(:priority, :integer)
    field(:is_default, :boolean, default: false)
    field(:created_by, :string)
    field(:updated_by, :string)
    timestamps(inserted_at_source: :created_at)
  end
end