defmodule Jitsu.ReverseSyncTargetOwner do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          target_hash: String.t(),
          workspace_id: String.t(),
          sync_id: String.t()
        }

  schema "reverse_sync_target_owner" do
    field(:target_hash, :string, primary_key: true)
    field(:workspace_id, :string)
    field(:sync_id, :string)
  end
end